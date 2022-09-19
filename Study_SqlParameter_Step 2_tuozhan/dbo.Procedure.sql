create proc GetPro
@Score char(10), --多个参数要用逗号分开
@Sex  nchar(10) output
as
begin
	select @Score=Score from Student where @Sex=Sex
end