using System.Text;
using System.Text.Json;
using System.Security.Cryptography;

namespace AuthServer
{
	public class AccessToken
	{
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member variables

		private Guid m_userId = Guid.Empty;
		private string m_sAccessSecret = string.Empty;

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Properties

		public Guid userId
		{
			get { return m_userId; }
		}

		public string accessSecret
		{
			get { return m_sAccessSecret; }
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Member functions

		public void Init(User user)
		{
			m_userId = user.userId;
			m_sAccessSecret = user.accessSecret;
		}

		public void Init()
		{
			m_userId = Guid.NewGuid();
			m_sAccessSecret = CreateAccessSecret();
		}

		private string CreateAccessSecret()
		{
			List<char> elements = s_secretElements.ToList();
			char[] accessSecret = new char[elements.Count];

			for (int i = 0; i < accessSecret.Length; i++)
			{
				int nCount = elements.Count;
				int nSelectNo = RandomUtil.Next(nCount);

				accessSecret[i] = elements[nSelectNo];
				elements.RemoveAt(nSelectNo);
			}

			return new string(accessSecret);
		}

		public string CreateToken()
		{
			//
			// 유저 데이터base64 변환
			//

			string playLoad = JsonSerializer.Serialize(new PayLoad { userId = m_userId.ToString(), accessSecret = m_sAccessSecret });
			string sPayloadToBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(playLoad));

			//
			// 해싱
			//

			byte[] securityKey = Encoding.UTF8.GetBytes(AppConfig.instance.AccessTokenSecurityKey);
			HMACMD5 hasher = new HMACMD5(securityKey);

			byte[] value = hasher.ComputeHash(Encoding.UTF8.GetBytes(sPayloadToBase64));
			string sSignature = Convert.ToBase64String(value);

			//
			// base64 데이터 및 해싱된 데이터 전달
			//

			StringBuilder sb = new StringBuilder();
			sb.Append(sPayloadToBase64);
			sb.Append('.');
			sb.Append(sSignature);

			return sb.ToString();
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		// Static member variables

		private static char[] s_secretElements = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
											   'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
											   'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
											   'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D',
											   'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
											   'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
											   'Y', 'Z' };
	}

	public class PayLoad
	{
		public string userId { get; set; } = string.Empty;
		public string accessSecret { get; set; } = string.Empty;
	}
}
