CREATE TABLE [Commands](
[id] int identity primary key,
[name] nvarchar(50),
[description] nvarchar(250)
)

CREATE TABLE [Players](
[id] int identity primary key,
[name] nvarchar(50),
[description] nvarchar(250)
)

CREATE TABLE [Match](
[id] int identity,
[name] nvarchar(50),
[description] nvarchar(250),
[command_one] int,
[command_two] int,
[points_team_one] int,
[points_team_two] int,
FOREIGN KEY([command_one]) REFERENCES [Commands]([id]),
FOREIGN KEY([command_two]) REFERENCES [Commands]([id])
)