CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS keeps(
  creatorId varchar(255) NOT NULL,
  id INT NOT NULL AUTO_INCREMENT primary key,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  description VARCHAR(255) COMMENT 'Description',
  img VARCHAR(255) COMMENT 'imgUrl',
  views INT DEFAULT 0,

  FOREIGN KEY (creatorId)
    REFERENCES accounts(id)
    ON DELETE CASCADE
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaults(
  creatorId varchar(255) NOT NULL,
  id INT NOT NULL AUTO_INCREMENT primary key,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  description varchar(255) COMMENT 'Description',
  img VARCHAR(255) COMMENT 'imgUrl',
  IsPrivate TINYINT DEFAULT 0,

  FOREIGN KEY (creatorId)
    REFERENCES accounts(id)
    ON DELETE CASCADE

  ) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS vaultKeeps(
  id INT NOT NULL AUTO_INCREMENT primary key,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  creatorId varchar(255) NOT NULL,
  vaultId INT NOT NULL,
  keepId INT NOT NULL,

  FOREIGN KEY (keepId)
    REFERENCES keeps(id)
    ON DELETE CASCADE,

      FOREIGN KEY (vaultId)
    REFERENCES vaults(id)
    ON DELETE CASCADE

  
) default charset utf8 COMMENT '';
