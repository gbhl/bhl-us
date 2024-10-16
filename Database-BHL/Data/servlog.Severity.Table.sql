SET IDENTITY_INSERT servlog.[Severity] ON 

INSERT servlog.Severity ([SeverityID], [Name], [Label], FGColorHexCode) VALUES (1, 'Information', 'Information', '#008000')
INSERT servlog.Severity ([SeverityID], [Name], [Label], FGColorHexCode) VALUES (2, 'Warning', 'Warning', '#FFFF00')
INSERT servlog.Severity ([SeverityID], [Name], [Label], FGColorHexCode) VALUES (3, 'Error', 'Error', '#FF0000')

SET IDENTITY_INSERT servlog.[Severity] OFF
