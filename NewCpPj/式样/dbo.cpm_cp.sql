CREATE TABLE [dbo].[cpm_cp](
	[league_name] [nvarchar](50) NOT NULL,
	[round] [int] NOT NULL,
	[game_idx] [int] NOT NULL,
	[game_date] [datetime] NOT NULL,
	[home_team_name] [nvarchar](50) NULL,
	[home_team_harf_score] [int] NOT NULL,
	[home_team_whole_score] [int] NOT NULL,
	[vist_team_name] [nvarchar](50) NULL,
	[vist_team_harf_score] [int] NOT NULL,
	[vist_team_whole_score] [int] NOT NULL,
    [pl_win] DECIMAL(18, 3) NULL, 
    [pl_ping] DECIMAL(18, 3) NULL, 
    [pl_lose] DECIMAL(18, 3) NULL, 
    [half_result] NVARCHAR(10) NULL, 
    [whole_result] NVARCHAR(10) NULL, 
    [weather] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_cpm_cp] PRIMARY KEY CLUSTERED 
(
	[league_name] ASC,
	[game_idx] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
