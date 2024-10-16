SET IDENTITY_INSERT servlog.Frequency ON 

INSERT servlog.Frequency ([FrequencyID], [Name], [Label], IntervalInMinutes) VALUES (1, 'EveryThree', '3 Minutes', 3)
INSERT servlog.Frequency ([FrequencyID], [Name], [Label], IntervalInMinutes) VALUES (2, 'EveryFive', '5 Minutes', 5)
INSERT servlog.Frequency ([FrequencyID], [Name], [Label], IntervalInMinutes) VALUES (3, 'EveryEight', '8 Minutes', 8)
INSERT servlog.Frequency ([FrequencyID], [Name], [Label], IntervalInMinutes) VALUES (4, 'Daily', 'Daily', 1440)
INSERT servlog.Frequency ([FrequencyID], [Name], [Label], IntervalInMinutes) VALUES (5, 'Weekly', 'Weekly', 10080)
INSERT servlog.Frequency ([FrequencyID], [Name], [Label], IntervalInMinutes) VALUES (6, 'Monthly', 'Monthly', 44640)

SET IDENTITY_INSERT servlog.Frequency OFF
