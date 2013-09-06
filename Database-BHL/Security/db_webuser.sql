CREATE ROLE [db_webuser]
    AUTHORIZATION [dbo];


GO
ALTER ROLE [db_webuser] ADD MEMBER [BHLWebUser];

