-- a rouler une couple de fois (nombre de table presente)
USE H15_PROJET_E03
GO

declare @stmt nvarchar(max)
-- procedures
select top 1 @stmt = isnull( @stmt + '
', '' ) + 'drop procedure ' + quotename(schema_name(schema_id)) + '.' + quotename(name)
from sys.procedures
 
-- check constraints
select top 1 @stmt = isnull( @stmt + '
', '' ) + 'alter table ' + quotename(schema_name(schema_id)) + '.' + quotename(object_name( parent_object_id )) + ' drop constraint ' + quotename(name)
from sys.check_constraints
 
-- functions
select top 1 @stmt = isnull( @stmt + '
', '' ) + 'drop function ' + quotename(schema_name(schema_id)) + '.' + quotename(name)
from sys.objects
where type in ( 'FN', 'IF', 'TF' )
 
-- views
select top 1 @stmt = isnull( @stmt + '
', '' ) + 'drop view ' + quotename(schema_name(schema_id)) + '.' + quotename(name)
from sys.views
 
-- foreign keys
select top 1 @stmt = isnull( @stmt + '
', '' ) + 'alter table ' + quotename(schema_name(schema_id)) + '.' + quotename(object_name( parent_object_id )) + ' drop constraint ' + quotename(name)
from sys.foreign_keys
 
-- tables
select top 1 @stmt = isnull( @stmt + '
', '' ) + 'drop table ' + quotename(schema_name(schema_id)) + '.' + quotename(name)
from sys.tables
 
-- user defined types
select top 1 @stmt = isnull( @stmt + '
', '' ) + 'drop type ' + quotename(schema_name(schema_id)) + '.' + quotename(name)
from sys.types
where is_user_defined = 1
 
-- schemas
select @stmt = isnull( @stmt + '
', '' ) + 'drop schema ' + quotename(name)
from sys.schemas
where principal_id <> schema_id
 
exec sp_executesql @stmt