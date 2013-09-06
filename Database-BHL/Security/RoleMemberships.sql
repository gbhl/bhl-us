ALTER ROLE [db_owner] ADD MEMBER [BotanicusService];


GO
ALTER ROLE [db_datareader] ADD MEMBER [BotanicusUserReader];


GO
ALTER ROLE [db_datareader] ADD MEMBER [BotanicusService];


GO
ALTER ROLE [db_datareader] ADD MEMBER [TropicosReadWrite];


GO
ALTER ROLE [db_datawriter] ADD MEMBER [BotanicusService];

