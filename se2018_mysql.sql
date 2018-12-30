CREATE TABLE BoardData(\
					BID CHAR(10),\
                    Name NVARCHAR(50),\
                    IsPublic BIT DEFAULT 0,\
                    Admin TEXT,\
                    PrivateMaster CHAR(10),\
                    PRIMARY KEY(BID));

CREATE TABLE ArticleData(\
					AID CHAR(10),\
                    Title MEDIUMTEXT,\
                    Content LONGTEXT,\
                    ReleaseUser CHAR(10),\
                    ReleaseDate VARCHAR(50),\
                    LikeCount INT DEFAULT 0,\
                    OfBoard MEDIUMTEXT,\
                    OfGroup CHAR(10),\
                    TbitLikeCount INT DEFAULT 0,\
                    PRIMARY KEY(AID));

CREATE TABLE AMessageData(\
					MID CHAR(10),\
					ReleaseUser CHAR(10),\
					ReleaseDate VARCHAR(10),\
					Context LONGTEXT,\
					LikeCount INT DEFAULT 0,\
					OfArticle CHAR(10),
					TbitLikeCount INT DEFAULT 0,\
					PRIMARY KEY(MID));

CREATE TABLE AdvertiseData(\
					DID VARCHAR(50),\
                    Body LONGBLOB,\
                    Location INT DEFAULT 0,\
					Size VARCHAR(50),\
					Deadline VARCHAR(50),\
					PRIMARY KEY(DID));

CREATE TABLE UserData(\
					UID CHAR(10),\
					ID VARCHAR(50),\
                    Password VARCHAR(50),\
                    Email VARCHAR(50),\
                    StudentNum VARCHAR(50),\
                    ClassName NVARCHAR(50),\
                    RealName NVARCHAR(50),\
                    NickName NVARCHAR(50),\
                    Picture LONGBLOB,\
                    Gender TINYINT DEFAULT 0,\
                    Birthday VARCHAR(50),\
                    UserPrivacy TINYINT DEFAULT 14,\
                    Friend TEXT,\
                    TbitCoin INT DEFAULT 0,\
                    FriendRequest TEXT,\
                    LastComputeTbit VARCHAR(50),\
                    FollowBoard TEXT,\
                    FollowBoardQueue TEXT,\
                    Viewstyle TINYINT DEFAULT 0,\
					MyBoard CHAR(10),\
                    PRIMARY KEY(UID));


