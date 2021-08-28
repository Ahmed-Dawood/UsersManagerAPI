# UsersManagerAPI
it's responsible for handling users operations for any platforms. it handles logging, verifying, registering and password related options using JWT Tokens and connects to your required users database

for this web api to work u need to do the following:
- set your mail and password in Maill class
- set your own DB connection string in appsettings.json
- set your own Domain Name in appsettings.json for confirm mail click url
- set you 32 char secret key that will be used across your APIs to verify your JWT tokens
- change the IUserInfo & UserInfo to fit your own DB structure with the same names to keep DI or change names and change DI in startup
