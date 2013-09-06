
CREATE FUNCTION [dbo].[fnGetFullTextSearchString](
	 @String nvarchar (4000)
	 )
RETURNS nvarchar(4000)
BEGIN
	/*
	 *  This purpose of this function is to split a search phrase into its
	 *  individual parts, add the necessary tokens for a full-text search,
	 *	and then put the individual parts back together.  
	 *	Terms/phrases delimited with double-quotes are kept together.
	 *  Terms/phrases delimited with parentheses are kept together.
	 *	Everything else is split into individual words.
	 *
	 *  Examples:	
	 *		input: birds							=> output: "*birds*"
	 *		input: game birds						=> output: "*game*" and "*birds*"
	 *		input: "game birds"						=> output: "game birds"
	 *		input: red "game birds"					=> output: "*red*" and "game birds"
	 *		input: darwin and (origin or descent)	=> output: "*darwin*" and (origin or descent)
	 */
	DECLARE @ReturnString nvarchar(4000)
	DECLARE @Pos int
	DECLARE @Pos2 int
	DECLARE @Delimiter nvarchar(1)
	DECLARE @Delimiter2 nvarchar(1)
	SET @ReturnString = ''
	SET @String = LTRIM(RTRIM(@String))
 
	DECLARE @ValueTable TABLE (Value nvarchar(4000))
 
	------------------------------------------------------------------------------------------------
	-- INSERT THE SEARCH STRING INTO A TABLE, BREAKING ANY DOUBLE-QUOTE-DELIMITED PARTS 
	-- OF THE SEARCH STRING OUT AS INDIVIDUAL PHRASES.  FOR EXAMPLE, THE SEARCH STRING 
	-- 'red "game birds"' RESULTS IN A TABLE WITH TWO ROWS: red AND "game birds"
	-- SIMILARLY, THE STRING 'red game birds' BECOMES A TABLE WITH ONE ROW: red game birds
 
	--Get the positions of the first pair of delimiters
	SET @Delimiter = '"'
	SET @Delimiter2 = '"'
	SET @Pos = CHARINDEX(@Delimiter,@String)
 
	IF (@Pos = 0) SET @Pos2 = 0
	ELSE IF (@Pos < len(@String)) SET @Pos2 = charindex(@Delimiter2,@String,@Pos + 1)
	ELSE SET @Pos2 = len(@String)
	
	-- Just in case there is an 'orphan' delimiter
 	IF (@Pos > 0 AND @Pos2 = 0) 
	BEGIN
		SET @String = @String + @Delimiter2
		SET @Pos2 = LEN(@String)
	END

	--Loop while there are still quote delimiters in the string
	WHILE (@Pos <>  0)  
	BEGIN
		-- Get everything prior to the first delimiter
		IF (@Pos > 1) INSERT INTO @ValueTable (Value) VALUES (SUBSTRING(@String, 1, @Pos - 1))
		
		-- Save the delimited phrase
		INSERT INTO @ValueTable (Value) VALUES (SUBSTRING(@String, @Pos, @Pos2 - @Pos + 1))

		-- Reset the search string to the end of the delimited phrase
		SET @String = SUBSTRING(@String,@pos2 + 1,LEN(@String))
		
		--Get the positions of the next pair of delimiters
		SET @Pos  = CHARINDEX(@Delimiter,@String)

		IF (@Pos = 0) SET @Pos2 = 0
		ELSE IF (@Pos < len(@String)) SET @Pos2 = charindex(@Delimiter2,@String,@Pos + 1)
		ELSE SET @Pos2 = len(@String)

		-- Just in case there is an 'orphan' delimiter
 		IF (@Pos > 0 AND @Pos2 = 0) 
		BEGIN
			SET @String = @String + @Delimiter2
			SET @Pos2 = LEN(@String)
		END
	END

	-- Get whatever is left over at the end of the original search string
	IF (LEN(@String) > 0) INSERT INTO @ValueTable (Value) VALUES (@String)

	------------------------------------------------------------------------------------------------
	-- NOW BREAK UP THE PHRASES WE'VE FOUND INTO INDIVIDUAL WORDS/TERMS, KEEPING WORDS BRACKETED 
	-- BY PARENTHESES AS INDIVIDUAL PHRASES.

	UPDATE @ValueTable SET Value = LTRIM(RTRIM(Value))	-- trim spaces
	DELETE @ValueTable WHERE Value = ''	-- remove empty strings
	
	DECLARE @ValueTable2 TABLE (Value nvarchar(4000))
	SET @Delimiter = '('
	SET @Delimiter2 = ')'
	
	DECLARE @NumValues int
	SELECT @NumValues = COUNT(Value) FROM @ValueTable
	
	-- Do this until we've processed all of the parts of the original search string
	WHILE (@NumValues > 0)
	BEGIN
		-- 'Pop' the first item off of our initial list of words
		SELECT TOP 1 @String = Value FROM @ValueTable
		DELETE FROM @ValueTable WHERE Value = @String
		
		-- If this is a double-quote-delimited phrase, just insert it into the final table
		IF (LEFT(@String, 1) = '"') 
		BEGIN
			INSERT @ValueTable2 (Value) VALUES (@String)
		END
		ELSE
		BEGIN
			--Get the positions of the first pair of parentheses delimiters
			SET @Pos = CHARINDEX(@Delimiter,@String)
		 
			IF (@Pos = 0) SET @Pos2 = 0
			ELSE IF (@Pos < len(@String)) SET @Pos2 = charindex(@Delimiter2,@String,@Pos + 1)
			ELSE SET @Pos2 = len(@String)
			
			-- Just in case there is an 'orphan' delimiter
 			IF (@Pos > 0 AND @Pos2 = 0) 
			BEGIN
				SET @String = @String + @Delimiter2
				SET @Pos2 = LEN(@String)
			END
			
			--Loop while there are still delimiters in the string
			WHILE (@Pos <>  0)  
			BEGIN
				-- Split non-delimited words at the start of the string, using space as the delimiter
				IF (@Pos > 1) INSERT INTO @ValueTable2 (Value) SELECT * FROM dbo.fn_Split(SUBSTRING(@String, 1, @Pos - 1), ' ')
				
				-- Save the delimited phrase as-is
				INSERT INTO @ValueTable2 (Value) VALUES (SUBSTRING(@String, @Pos, @Pos2 - @Pos + 1))

				-- Reset the search string to the end of the delimited phrase
				SET @String = SUBSTRING(@String,@pos2 + 1,LEN(@String))
				
				--Get the positions of the next pair of delimiters
				SET @Pos  = CHARINDEX(@Delimiter,@String)

				IF (@Pos = 0) SET @Pos2 = 0
				ELSE IF (@Pos < len(@String)) SET @Pos2 = charindex(@Delimiter2,@String,@Pos + 1)
				ELSE SET @Pos2 = len(@String)

				-- Just in case there is an 'orphan' delimiter
 				IF (@Pos > 0 AND @Pos2 = 0) 
				BEGIN
					SET @String = @String + @Delimiter2
					SET @Pos2 = LEN(@String)
				END
			END

			-- Split whatever is left over at the end of the string, using space as the delimiter
			IF (LEN(@String) > 0) INSERT INTO @ValueTable2 (Value) SELECT Value FROM dbo.fn_Split(@String, ' ')
		END	

		-- See if there is anything left to evaluate
		SELECT @NumValues = COUNT(Value) FROM @ValueTable	
	END	
	
	------------------------------------------------------------------------------------------------
	-- CLEAN UP THE SEARCH WORDS/PHRASES AND ADD FULL-TEXT SEARCH TOKENS

	UPDATE @ValueTable2 SET Value = LTRIM(RTRIM(Value))	-- trim spaces
	DELETE @ValueTable2 WHERE Value = ''	-- remove empty strings
	DELETE @ValueTable2 WHERE Value = '&'	-- remove ampersands, as they are not indexed
	DELETE @ValueTable2 WHERE LEN(Value) = 1	-- remove single letters; full-text search doesn't always handle them well

	-- Bracket each word with full-text search tokens
	UPDATE @ValueTable2 
	SET Value = '"*' + Value + '*"' 
	WHERE Value NOT IN ('or', 'and')	-- bracket all non-operators
	AND CHARINDEX('"', Value) = 0		-- bracket all non-phrases
	AND CHARINDEX('(', Value) = 0		-- bracket all non-phrases

	-- PUT THE SEARCH STRING BACK TOGETHER
	 
	-- Put the individual search elements back together into a single string
	SELECT @ReturnString = @ReturnString + Value FROM @ValueTable2

	-- Put "and" operators in where necessary
	SET @ReturnString = REPLACE(@ReturnString, '""', '" and "')
	
	-- Set the appropriate full-text search token for an empty search string
	IF (@ReturnString = '') SET @ReturnString = '"**"'

	RETURN @ReturnString
END



