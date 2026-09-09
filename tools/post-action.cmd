ECHO "Start creating User JWTS!"
dotnet user-jwts create --name "b0788d2f-8003-43c1-92a4-edc76a7c5dde" --audience "sfc.game" --issuer "https://localhost:7266" --scope "sfc.game.full" --scope "profile" --scope "openid" --scope "offline_access" --valid-for 365d --project "src/API/SFC.Game.Api/SFC.Game.Api.csproj"
ECHO "User JWTS creation finished!"

ECHO "Start generating certificate!"
wsl ./generate-service-certificate.sh sfc-game
ECHO "Generating certificate finished!"

ECHO "Solution successfully created!"