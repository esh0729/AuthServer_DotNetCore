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
		// 데이터베이스 동기 호출이므로 Login함수 비동기로 호출 필요
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

				GuestUser? guestUser = guestUsers.Find(id);
				if (guestUser != null)
				{
					User? user = users.Find(guestUser.userId);
					if (user != null)
						accessToken.Init(user);
					else
						throw new Exception("Not Exist User");
				}
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
