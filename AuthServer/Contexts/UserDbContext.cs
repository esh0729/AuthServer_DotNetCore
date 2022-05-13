using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text.Json;

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

		//
		// System Data
		//

		public DbSet<SystemSetting> systemSettings { get; set; }
		public DbSet<GameServer> gameServers { get; set; }

		//
		// Transaction Data
		//

		public DbSet<User> users { get; set; }
		public DbSet<GuestUser> guestUsers { get; set; }

		//
		// Resource Data
		//
		#region Resource Data
		public DbSet<GameConfig> gameConfigs { get; set; }
		public DbSet<Continent> continents { get; set; }
		public DbSet<Character> characters { get; set; }
		public DbSet<CharacterAction> characterActions { get; set; }
		#endregion

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member functions

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//
			// 다중키 설정
			//

			modelBuilder.Entity<CharacterAction>().HasKey(p => new { p.characterId, p.actionId });
		}

		//
		// Login
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
					//var query = from guestUser in guestUsers
					//			join user in users
					//				on guestUser.userId equals user.userId
					//			where guestUser.guestId == id
					//			select user;

					//result = query.ToList();

					//var query = guestUsers.Join(users,
					//							guestUser => guestUser.userId,
					//							user => user.userId,
					//							(guestUser, user) =>
					//							new
					//							{
					//								guestUserId = guestUser.guestId,
					//								userId = user.userId,
					//								accessSecret = user.accessSecret
					//							}).Where(guestUser => guestUser.guestUserId == id).
					//							Select(result =>
					//							new User
					//							{
					//								userId = result.userId,
					//								accessSecret = result.accessSecret
					//							});

					var query = guestUsers.Where(guestUser => guestUser.guestId == id).
											Join(users,
												guestUser => guestUser.userId,
												user => user.userId,
												(guestUser, user) =>
												new User
												{
													userId = user.userId,
													accessSecret = user.accessSecret
												});


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
				// 작업중 에러 났을 경우 롤백
				//

				if (trans != null)
					trans.Rollback();
			}
		}

		//
		// Systeminfo
		//

		public string SystemInfo()
		{
			//
			// 시스템세팅 조회
			//

			List<SystemSetting> systemSettingList = systemSettings.ToList();
			if (systemSettingList.Count <= 0)
				throw new Exception("Not exist SystemSetting");

			//
			// 게임서버 조회
			//

			List<GameServer> gameServerList = gameServers.OrderBy(r => r.gameServerId).ToList();

			SystemInfoTemplate template = new SystemInfoTemplate();
			template.systemSetting = systemSettingList.First();
			template.gameServers = new GameServer[gameServerList.Count];

			for (int i = 0; i < gameServerList.Count; i++)
			{
				template.gameServers[i] = gameServerList[i];
			}

			return JsonSerializer.Serialize(template);
		}

		//
		// MetaData
		//

		public string MetaData()
		{
			//
			// 시스템세팅 조회
			//

			List<SystemSetting> systemSettingList = systemSettings.ToList();
			if (systemSettingList.Count <= 0)
				throw new Exception("Not exist SystemSetting");

			//
			// 메타데이터 버전 확인및 데이터 로드
			//

			int nMetaDataVersion = systemSettingList.First().metadataVersion;

			return MetaDataManager.GetMetaData(nMetaDataVersion, this);
		}
	}
}
