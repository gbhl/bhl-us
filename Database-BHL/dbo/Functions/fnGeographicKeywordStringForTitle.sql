CREATE FUNCTION [dbo]. [fnGeographicKeywordStringForTitle]
(
        @TitleID int
)
RETURNS nvarchar (1024)
AS
BEGIN
        DECLARE @KeywordString nvarchar( 1024)

        SELECT @KeywordString = COALESCE(@KeywordString , '' ) + Keyword + '|'
        FROM   (
                      -- Get all tags tied directly to this title
                      SELECT DISTINCT
                                   Keyword
                      FROM   TitleKeywordView
                      WHERE  TitleID = @TitleID
                      AND           ((MarcDataFieldTag = '651' AND MarcSubFieldCode = 'a' )
                      OR            (MarcDataFieldTag IN ('600', '610', '611', '630', '648', '650') AND MarcSubFieldCode = 'z'))
                      ) X
        ORDER BY
                      Keyword ASC

        RETURN LTRIM (RTRIM( COALESCE(@KeywordString , '' )))
END
