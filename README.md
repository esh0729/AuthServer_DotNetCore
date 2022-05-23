# AuthServer_DotNetCore
.NET Core 기반의 인증 및 메타데이터 호출 서버

프로젝트 설명
- WebCommon : 클라이언트에 필요한 메타데이터 프로토콜
- AuthServer : 요청 구현부

요청 설명
- 각 요청에 대응하는 URI를 호출하여 데이터 요청
- SystemInfo : 시스템 관련 데이트 요청(GET)
- metadat : 인게임 메타데이터 요청(GET)
- login : 게임서버에 접속할 accessToken 요청, 접근시 id 필요(POST)
