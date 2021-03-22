IF (OBJECT_ID('WarehouseLot', 'U') IS NULL)
BEGIN
    CREATE TABLE [WarehouseLot] (
        [Id] INT NOT NULL IDENTITY(1,1),
        [Name] NVARCHAR(100) NOT NULL,
        [Description] NVARCHAR(500) NULL,
        [Type] NVARCHAR(50) NOT NULL,           -- 'freezer', 'dry'
        [Occupated] NVARCHAR(50) NOT NULL,      -- 'empty', 'has-items', 'full' 
        [WeightCapacity] DECIMAL(19,2) NOT NULL,
        [LastInventoryChange] DATETIME NULL,
        [Manager_FirstName] NVARCHAR(100) NOT NULL,
        [Manager_LastName] NVARCHAR(100) NOT NULL,
        [Manager_Phone] NVARCHAR(100) NULL,
        [Manager_Email] NVARCHAR(100) NULL,
        CONSTRAINT [PK_WarehouseLot] PRIMARY KEY CLUSTERED ([Id])
    );
END;

IF (OBJECT_ID('ParcelType', 'U') IS NULL)
BEGIN
    CREATE TABLE [ParcelType] (
        [Id] INT NOT NULL IDENTITY(1,1),
        [Name] NVARCHAR(100) NOT NULL,
        [Description] NVARCHAR(500) NULL,
        [MinWeightOccupied] DECIMAL(19,2) NOT NULL,     -- KG
        [MaxWeight] DECIMAL(19,2) NOT NULL,             -- KG
        [IsPerishable] BIT NOT NULL,                    -- Can spoil
        [FreezerLifetime] INT NULL,                     -- DAYS; only used if IsPerishable; if null then cannot go into freezer lot
        [DryLifetime] INT NULL,                         -- DAYS; only used if IsPerishable; if null then cannot go into dry lot
        CONSTRAINT [PK_ParcelType] PRIMARY KEY CLUSTERED ([Id])
    );
END;

IF (OBJECT_ID('Supplier', 'U') IS NULL)
BEGIN
    CREATE TABLE [Supplier] (
        [Id] INT NOT NULL IDENTITY(1,1),
        [Name] NVARCHAR(100) NOT NULL,
        [Description] NVARCHAR(500) NULL,
        [Contact_FirstName] NVARCHAR(100) NULL,
        [Contact_LastName] NVARCHAR(100) NULL,
        [Contact_Phone] NVARCHAR(100) NULL,
        [Contact_Email] NVARCHAR(100) NULL,
        [Contact_Fax] NVARCHAR(100) NULL,
        [Address_State] NVARCHAR(100) NULL,
        [Address_Country] NVARCHAR(100) NULL,
        [Address_PostalCode] NVARCHAR(100) NULL,
        [Address_AddressLine1] NVARCHAR(100) NULL,
        [Address_AddressLine2] NVARCHAR(100) NULL,
        [AddDate] DATETIME NOT NULL,        -- AUTOVALUE
        [FirstContractDate] DATETIME NULL,
        [LastContractDate] DATETIME NULL,
        CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED ([Id])
    );
END;

IF (OBJECT_ID('Contract', 'U') IS NULL)
BEGIN
    CREATE TABLE [Contract] (
        [Id] INT NOT NULL IDENTITY(1,1),
        [SupplierId] INT NOT NULL,
        [Description] NVARCHAR(500) NOT NULL,
        [StartDate] DATETIME NOT NULL,
        [PaymentDueUntil] DATETIME NOT NULL,
        [IsPayed] BIT NOT NULL,
        [AddDate] DATETIME NOT NULL,        -- AUTOVALUE
        CONSTRAINT [PK_Contract] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [FK_Contract_Supplier_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Supplier] ([Id])
    );
END;

IF (OBJECT_ID('Parcel', 'U') IS NULL)
BEGIN
    CREATE TABLE [Parcel] (
        [Id] INT NOT NULL IDENTITY(1,1),
        [ContractId] INT NOT NULL,
        [ParcelTypeId] INT NOT NULL,
        [WarehouseLotId] INT NOT NULL,
        [Weight] DECIMAL(19,2) NULL,
        [AddDate] DATETIME NOT NULL,        -- AUTOVALUE
        [ValidUntil] DATETIME NULL,     -- maybe this should be composite?
        [IsRemoved] BIT NOT NULL,
        [RemovedDate] DATETIME NULL,
        CONSTRAINT [PK_Parcel] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [FK_Parcel_Contract_ContractId] FOREIGN KEY ([ContractId]) REFERENCES [Contract] ([Id]),
        CONSTRAINT [FK_Parcel_ParcelType_ParcelTypeId] FOREIGN KEY ([ParcelTypeId]) REFERENCES [ParcelType] ([Id]),
        CONSTRAINT [FK_Parcel_WarehouseLot_WarehouseLotId] FOREIGN KEY ([WarehouseLotId]) REFERENCES [WarehouseLot] ([Id])
    );
END;
