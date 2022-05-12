using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AuthServer
{
	public class UserDbContext : DbContext
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Constructors

		public UserDbContext(DbContextOptions<UserDbContext> options)
			:base(options)
		{
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Properties

		public DbSet<User> users { get; set; }
		public DbSet<GuestUser> guestUsers { get; set; }

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member functions

		//
		// Login함수 비동기로 호출 필요
		//

		public string Login(Guid id)
		{
			IDbContextTransaction? trans = null;

			try
			{
				AccessToken accessToken = new AccessToken();

				//
				// 사용자 조회
				//

				List<User> result;

				{
					var query = from guestUser in Set<GuestUser>()
								join user in Set<User>()
									on guestUser.userId equals user.userId
								where guestUser.guestId == id
								select user;

					result = query.ToList();
				}		

				//
				//
				//

				if (result.Count() > 0)
					accessToken.Init(result.First());
				else
				{
					//
					// 해당ID의 유저가 없을경우 생성
					//

					accessToken.Init();

					//
					// 트랜젝션
					//

					trans = Database.BeginTransaction();

					users.Add(new User { userId = accessToken.userId, accessSecret = accessToken.accessSecret });
					guestUsers.Add(new GuestUser { guestId = id, userId = accessToken.userId });

					SaveChanges();

					//
					// 테이블 저장 후 커밋
					//

					trans.Commit();
					trans = null;
				}

				return accessToken.CreateToken();
			}
			catch
			{
				throw;
			}
			finally
			{
				//
				// 테이블 삽입중 에러 났을 경우 롤백
				//

				if (trans != null)
					trans.Rollback();
			}
		}
	}
}
