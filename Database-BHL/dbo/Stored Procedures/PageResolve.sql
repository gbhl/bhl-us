
CREATE PROCEDURE [dbo].[PageResolve] 
	@TitleID	int,
	@Volume	nvarchar(20),
	@Issue	nvarchar(20),
	@Year	nvarchar(20),
	@StartPage	nvarchar(20)
AS

SET NOCOUNT ON

IF (@Volume = '') SELECT @Volume = NULL
IF (@Issue = '') SELECT @Issue = NULL
IF (@Year = '') SELECT @Year = NULL



create table #items
(
	ItemID int
)

declare @ItemID int, @ItemCount int
select @ItemCount = 0
--see if we can narrow it down to an item
if @Volume is not null
begin
	insert into #items 
	select distinct p.ItemID
	from Page p
	join Item i on p.ItemID = i.ItemID
	join TitleItem ti on i.ItemID = ti.ItemID
	join Title t on ti.TitleID = t.TitleID
	where t.TitleID = @TitleID and
		t.PublishReady = 1 and
		p.Volume = @Volume

	select @ItemCount = count(*) from #items
	if @ItemCount = 0
	begin
		insert into #items 
		select distinct p.ItemID
		from Page p
		join Item i on p.ItemID = i.ItemID
		join TitleItem ti on i.ItemID = ti.ItemID
		join Title t on ti.TitleID = t.TitleID
		where t.TitleID = @TitleID and
			t.PublishReady = 1 and
			(
				i.Volume like '%v. ' + @Volume + '%' or
				i.Volume like '%v.' + @Volume + '%' or
				i.Volume like '%v ' + @Volume + '%' or
				i.Volume like '%v' + @Volume + '%' or
				i.Volume like '%vol. ' + @Volume + '%' or
				i.Volume like '%vol.' + @Volume + '%' or
				i.Volume like '%vol ' + @Volume + '%' or
				i.Volume like '%vol' + @Volume + '%'
			)
		select @ItemCount = count(*) from #items
	end
end

CREATE TABLE #pages
(
	PageID INT,
	ItemID INT,
	ItemSequence INT,
	SequenceOrder INT,
	Issue nvarchar(20),
	Year nvarchar(20),
	PagePrefix nvarchar(40),
	PageNumber nvarchar(20),
	Volume nvarchar(20),
	FullTitle ntext,
	ItemVolume nvarchar(100)
)

--if a volume was provided and we have no item matches then we need to drop out because
--we have no way to determine the correct volume
if @Volume is not null and @ItemCount = 0
begin
	select * from #pages
	drop table #items
	drop table #pages
	return
end

--do our initial population of the #pages table
if @StartPage is not null and @StartPage <> ''
begin
	if @ItemCount > 0
	begin
		insert into #pages
		select p.PageID, it.ItemID, ti.ItemSequence, p.SequenceOrder, p.Issue, 
			p.Year, ip.PagePrefix, ip.PageNumber, p.Volume, t.FullTitle, i.Volume
		from #items it
		join Item i on it.ItemID = i.ItemID
		join Page p on i.ItemID = p.ItemID
		join IndicatedPage ip on p.PageID = ip.PageID
		join TitleItem ti ON i.ItemID = ti.ItemID
		join Title t on ti.TitleID = t.TitleID
		where ip.PageNumber = @StartPage and
			p.Active = 1
	end
	else
	begin
		insert into #pages
		select p.PageID, i.ItemID, ti.ItemSequence, p.SequenceOrder, p.Issue, 
			p.Year, ip.PagePrefix, ip.PageNumber, p.Volume, t.FullTitle, i.Volume
		from Item i
		join Page p on i.ItemID = p.ItemID
		join IndicatedPage ip on p.PageID = ip.PageID
		join TitleItem ti on i.ItemID = ti.ItemID
		join Title t on ti.TitleID = t.TitleID
		where t.TitleID = @TitleID and
			ip.PageNumber = @StartPage and
			p.Active = 1
	end

	--if no pages were inserted based on start page, then look for a title page
	if (select count(*) from #pages) = 0
	begin
		if @ItemCount > 0
		begin
			insert into #pages
			select p.PageID, it.ItemID, ti.ItemSequence, p.SequenceOrder, p.Issue, 
				p.Year, ip.PagePrefix, ip.PageNumber, p.Volume, t.FullTitle, i.Volume
			from #items it
			join Item i on it.ItemID = i.ItemID
			join Page p on i.ItemID = p.ItemID
			left join IndicatedPage ip on p.PageID = ip.PageID
			join Page_PageType ppt on p.PageID = ppt.PageID
			join PageType pt on ppt.PageTypeID = pt.PageTypeID
			join TitleItem ti on i.ItemID = ti.ItemID
			join Title t on ti.TitleID = t.TitleID
			where pt.PageTypeID = 1 and
				p.Active = 1
		end
		else
		begin
			insert into #pages
			select p.PageID, i.ItemID, ti.ItemSequence, p.SequenceOrder, p.Issue, 
				p.Year, ip.PagePrefix, ip.PageNumber, p.Volume, t.FullTitle, i.Volume
			from Item i
			join Page p on i.ItemID = p.ItemID
			left join IndicatedPage ip on p.PageID = ip.PageID
			join Page_PageType ppt on p.PageID = ppt.PageID
			join PageType pt on ppt.PageTypeID = pt.PageTypeID
			join TitleItem ti on i.ItemID = ti.ItemID
			join Title t on ti.TitleID = t.TitleID
			where t.TitleID = @TitleID and
				pt.PageTypeID = 1 and
				p.Active = 1
		end
	end
end
else
begin
	--if we weren't given a start page, look for title pages
	if @ItemCount > 0
	begin
		insert into #pages
		select p.PageID, it.ItemID, ti.ItemSequence, p.SequenceOrder, p.Issue, 
			p.Year, ip.PagePrefix, ip.PageNumber, p.Volume, t.FullTitle, i.Volume
		from #items it
		join Item i on it.ItemID = i.ItemID
		join Page p on i.ItemID = p.ItemID
		left join IndicatedPage ip on p.PageID = ip.PageID
		join Page_PageType ppt on p.PageID = ppt.PageID
		join PageType pt on ppt.PageTypeID = pt.PageTypeID
		join TitleItem ti on i.ItemID = ti.ItemID
		join Title t on ti.TitleID = t.TitleID
		where pt.PageTypeID = 1 and
			p.Active = 1
	end
	else
	begin
		insert into #pages
		select p.PageID, i.ItemID, ti.ItemSequence, p.SequenceOrder, p.Issue, 
			p.Year, ip.PagePrefix, ip.PageNumber, p.Volume, t.FullTitle, i.Volume
		from Item i
		join Page p on i.ItemID = p.ItemID
		left join IndicatedPage ip on p.PageID = ip.PageID
		join Page_PageType ppt on p.PageID = ppt.PageID
		join PageType pt on ppt.PageTypeID = pt.PageTypeID
		join TitleItem ti on i.ItemID = ti.ItemID
		join Title t on ti.TitleID = t.TitleID
		where t.TitleID = @TitleID and
			pt.PageTypeID = 1 and
			p.Active = 1
	end
end

if (select count(*) from #pages) = 0 and (select count(*) from #items) > 0
begin
	insert into #pages
	select null, it.ItemID, null, null, null, null, null, null, null, t.FullTitle, i.Volume
	from #items it
	join Title t on t.TitleID = @TitleID
	join Item i on it.ItemID = i.ItemID
end
--if we got an issue, drop any rows from our #pages table that don't match
if @Issue is not null
begin
	delete from #pages where Issue <> @Issue
end

--if we got a year, drop any rows from our #pages table that don't match
if @Year is not null
begin
	--if deleting based on year would wipe out all of our results, then don't filter on year
	if (select count(*) from #pages where Year = @Year) > 0
		delete from #pages where Year <> @Year
end

select * from #pages

