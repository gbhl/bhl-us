CREATE FUNCTION [dbo].[fnGetSortString]
(
	@strText VARCHAR(MAX)
)
RETURNS VARCHAR(MAX)
AS
BEGIN
-- Per the guidelines at https://www.niso.org/sites/default/files/2017-08/tr03.pdf
--		Replace with spaces:  - / \
--		Ignore (remove):  . , ; : ( ) [ ] < > { } ' " ! ?
--		Leave as-is:  letters, numbers, all symbols not listed in previous lists
-- In order to handle some non-English languages, move the apostrophe (') to the "Replace with spaces" list
--		For example, the French "d'amphipodes" becomes "d amphipodes", and not "damphipodes".
	SET @strText = REPLACE(@strText, '''', ' ')
	SET @strText = REPLACE(@strText, '-', ' ')
	SET @strText = REPLACE(@strText, '/', ' ')
	SET @strText = REPLACE(@strText, '\', ' ')

	SET @strText = REPLACE(@strText, '.', '')
	SET @strText = REPLACE(@strText, ',', '')
	SET @strText = REPLACE(@strText, ';', '')
	SET @strText = REPLACE(@strText, ':', '')
	SET @strText = REPLACE(@strText, '(', '')
	SET @strText = REPLACE(@strText, ')', '')
	SET @strText = REPLACE(@strText, '[', '')
	SET @strText = REPLACE(@strText, ']', '')
	SET @strText = REPLACE(@strText, '{', '')
	SET @strText = REPLACE(@strText, '}', '')
	SET @strText = REPLACE(@strText, '<', '')
	SET @strText = REPLACE(@strText, '>', '')
	SET @strText = REPLACE(@strText, '"', '')
	SET @strText = REPLACE(@strText, '!', '')
	SET @strText = REPLACE(@strText, '?', '')

	/*	
	SET @strText = REPLACE(@strText, '''', ' ')
	SET @strText = REPLACE(@strText, '-', ' ')

    WHILE PATINDEX('%[^ ^0-9^a-z^a-Z]%', @strText) > 0
    BEGIN
        SET @strText = STUFF(@strText, PATINDEX('%[^ ^0-9^a-z^a-Z]%', @strText), 1, '')
    END
	*/

	-- Method for replacing instances of multiple spaces with a single space
	-- taken from https://www.sqlservercentral.com/forums/reply/1081230
	SET @strText = replace(replace(replace(replace(replace(replace(replace(ltrim(rtrim(@strText)),
		'                                 ',' '),
		'                 ',' '),
		'         ',' '),
		'     ',' '),
		'   ',' '),
		'  ',' '),
		'  ',' ')

    RETURN RTRIM(LTRIM(@strText))
END
