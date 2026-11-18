CREATE PROCEDURE [dbo].[IAItemSelectByStatus]
    @ItemStatusID  int            = -1,
    @IAIdentifier  nvarchar(200)  = '',
    @NumRows       int            = 100,
    @PageNum       int            = 1,
    @SortColumn    nvarchar(150)  = 'IAIdentifier',
    @SortDirection nvarchar(4)    = 'ASC'
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF @NumRows  < 1 SET @NumRows = 100;
        IF @PageNum  < 1 SET @PageNum = 1;

        DECLARE @Offset      int           = (@PageNum - 1) * @NumRows;
        DECLARE @UpperBound  bigint        = CONVERT(bigint, @Offset) + @NumRows;
        DECLARE @Pattern     nvarchar(201) = @IAIdentifier + N'%';
        DECLARE @Dir         nvarchar(4)   = CASE WHEN LOWER(@SortDirection) = N'desc' THEN N'DESC' ELSE N'ASC' END;

        ------------------------------------------------------------------
        -- Reusable SQL fragments
        ------------------------------------------------------------------
        DECLARE @ScanDateExpr nvarchar(max) = N'ISNULL(CASE
                WHEN LEN(i.ScanDate) = 14 THEN SUBSTRING(i.ScanDate, 1, 4) + ''-'' + SUBSTRING(i.ScanDate, 5, 2) + ''-'' + SUBSTRING(i.ScanDate, 7, 2) + '' '' +
                                               SUBSTRING(i.ScanDate, 9, 2) + '':'' + SUBSTRING(i.ScanDate, 11, 2) + '':'' + SUBSTRING(i.ScanDate, 13, 2)
                WHEN LEN(i.ScanDate) = 8  THEN SUBSTRING(i.ScanDate, 1, 4) + ''-'' + SUBSTRING(i.ScanDate, 5, 2) + ''-'' + SUBSTRING(i.ScanDate, 7, 2)
                WHEN LEN(i.ScanDate) = 0  THEN NULL
                ELSE i.ScanDate
            END, N'''')';

        DECLARE @HoldApply nvarchar(max) = N'
            OUTER APPLY (SELECT TOP (1) m.DCElementValue AS v
                         FROM dbo.IADCMetadata m
                         WHERE m.ItemID = i.ItemID AND m.DCElementName = N''contributor'' AND m.[Source] = N''META''
                         ORDER BY m.DCMetadataID) mm
            OUTER APPLY (SELECT TOP (1) m.DCElementValue AS v
                         FROM dbo.IADCMetadata m
                         WHERE m.ItemID = i.ItemID AND m.DCElementName = N''contributor'' AND m.[Source] = N''DC''
                         ORDER BY m.DCMetadataID) md';

        DECLARE @HoldExpr nvarchar(max) = N'ISNULL(COALESCE(NULLIF(mm.v, N''''), md.v), N'''')';

        DECLARE @StatusJoin nvarchar(max) = N'
            INNER JOIN dbo.IAItemStatus s ON i.ItemStatusID = s.ItemStatusID';
        DECLARE @UserJoin nvarchar(max) = N'
            LEFT JOIN dbo.BHLAspNetUsers uc ON i.CreatedUserID = uc.Id
            LEFT JOIN dbo.BHLAspNetUsers um ON i.LastModifiedUserID = um.Id';

        ------------------------------------------------------------------
        -- Sort whitelist: expression + the joins that expression needs
        ------------------------------------------------------------------
        DECLARE @SortExpr nvarchar(max), @SortJoin nvarchar(max) = N'';

        IF      @SortColumn = N'ExternalStatus'         SET @SortExpr = N'i.ExternalStatus';
        ELSE IF @SortColumn = N'Status'                 SELECT @SortExpr = N's.[Status]', @SortJoin = @StatusJoin;
        ELSE IF @SortColumn = N'HoldingInstitution'     SELECT @SortExpr = @HoldExpr,     @SortJoin = @HoldApply;
        ELSE IF @SortColumn = N'IAAddedDate'            SET @SortExpr = N'i.IAAddedDate';
        ELSE IF @SortColumn = N'ScanDate'               SET @SortExpr = @ScanDateExpr;
        ELSE IF @SortColumn = N'IADateStamp'            SET @SortExpr = N'i.IADateStamp';
        ELSE IF @SortColumn = N'LastXMLDataHarvestDate' SET @SortExpr = N'i.LastXMLDataHarvestDate';
        ELSE IF @SortColumn = N'LastProductionDate'     SET @SortExpr = N'i.LastProductionDate';
        ELSE IF @SortColumn = N'CreatedDate'            SET @SortExpr = N'i.CreatedDate';
        ELSE IF @SortColumn = N'LastModifiedDate'       SET @SortExpr = N'i.LastModifiedDate';
        ELSE IF @SortColumn = N'CreatedUser'            SELECT @SortExpr = N'LTRIM(uc.FirstName + N'' '' + uc.LastName)', @SortJoin = @UserJoin;
        ELSE IF @SortColumn = N'LastModifiedUser'       SELECT @SortExpr = N'LTRIM(um.FirstName + N'' '' + um.LastName)', @SortJoin = @UserJoin;
        ELSE                                            SET @SortExpr = N'i.IAIdentifier';   -- default / IAIdentifier

        ------------------------------------------------------------------
        -- Optional predicates (only added when the parameter is in use)
        ------------------------------------------------------------------
        DECLARE @Where nvarchar(max) = N'1 = 1';
        IF @ItemStatusID <> -1 SET @Where += N' AND i.ItemStatusID = @ItemStatusID';
        IF @IAIdentifier <> N'' SET @Where += N' AND i.IAIdentifier LIKE @Pattern';

        ------------------------------------------------------------------
        -- Final statement
        ------------------------------------------------------------------
        DECLARE @Sql nvarchar(max) = N'
;WITH Paged AS
(
    -- Narrow stage: filter + sort + number on keys only
    SELECT  i.ItemID,
            ROW_NUMBER() OVER (ORDER BY ' + @SortExpr + N' ' + @Dir + N', i.ItemID) AS RowNumber,
            COUNT(*) OVER ()                                                       AS TotalItems
    FROM    dbo.IAItem i' + @SortJoin + N'
    WHERE   ' + @Where + N'
)
-- Wide stage: only for the rows on the requested page
SELECT  i.ItemID
       ,i.IAIdentifier
       ,ISNULL(i.ExternalStatus, '''')              AS ExternalStatus
       ,s.[Status]
       ,ISNULL(i.ShortTitle, '''')                  AS ShortTitle
       ,ISNULL(i.Volume, '''')                      AS Volume
       ,i.[Year]
       ,i.ImageCount
       ,' + @HoldExpr + N'                         AS HoldingInstitution
       ,ISNULL(i.Sponsor, '''')                     AS Sponsor
       ,ISNULL(i.ScanningInstitution, '''')         AS ScanningInstitution
       ,ISNULL(i.RightsHolder, '''')                AS RightsHolder
       ,ISNULL(i.Note, '''')                        AS Note
       ,ISNULL(i.LicenseUrl, '''')                  AS LicenseUrl
       ,ISNULL(i.Rights, '''')                      AS Rights
       ,ISNULL(i.DueDiligence, '''')                AS DueDiligence
       ,ISNULL(i.PossibleCopyrightStatus, '''')     AS PossibleCopyrightStatus
       ,i.VirtualTitleID
       ,i.VirtualVolume
       ,i.IAAddedDate
       ,' + @ScanDateExpr + N'                     AS ScanDate
       ,i.IADateStamp
       ,i.LastXMLDataHarvestDate
       ,i.LastProductionDate
       ,i.CreatedDate
       ,i.LastModifiedDate
       ,LTRIM(uc.FirstName + N'' '' + uc.LastName)   AS CreatedUser
       ,LTRIM(um.FirstName + N'' '' + um.LastName)   AS LastModifiedUser
       ,p.TotalItems
FROM    Paged p
        INNER JOIN dbo.IAItem i       ON p.ItemID = i.ItemID
        INNER JOIN dbo.IAItemStatus s ON i.ItemStatusID = s.ItemStatusID' + @HoldApply + N'
        LEFT JOIN dbo.BHLAspNetUsers uc  ON i.CreatedUserID = uc.Id
        LEFT JOIN dbo.BHLAspNetUsers um  ON i.LastModifiedUserID = um.Id
WHERE   p.RowNumber >  @Offset
AND     p.RowNumber <= @UpperBound
ORDER BY p.RowNumber;';

        EXEC sys.sp_executesql
              @Sql
            , N'@ItemStatusID int, @Pattern nvarchar(201), @Offset int, @UpperBound bigint'
            , @ItemStatusID = @ItemStatusID
            , @Pattern      = @Pattern
            , @Offset       = @Offset
            , @UpperBound   = @UpperBound;

        RETURN 0;
    END TRY
    BEGIN CATCH
        -- Re-throws the original error (original proc returned 9 / RAISERROR)
        THROW;
    END CATCH
END
GO
