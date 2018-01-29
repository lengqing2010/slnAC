CREATE TABLE [m_siryou](
	[edp_no] [varchar](20) NOT NULL,
	[group_nm] [varchar](200) NOT NULL,
	[file_nm] [varchar](200) NOT NULL,
	[txt] [ntext] NULL,
	[user_id] [varchar](100) NULL,
	[type] [varchar](20) NULL,
	[ins_time] [datetime] NULL
 CONSTRAINT [PK_m_siryoiu] PRIMARY KEY CLUSTERED 
(
	[edp_no] ASC,
	[group_nm] ASC,
	[file_nm]
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
