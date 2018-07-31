# MongOData_Csharp

Disclaimer: This repository is part of a university project paper with the title ```Feasibility of an OData Interface on a NoSQL database using the example of a MongoDB```. If interested, you can contact me and I will provide you with the complete documentation of my research.

Also checkout the other respository linked to the research: https://github.com/memmelma/MongOData_NodeJS


## Setup
Clone this repo and open it with Visual Studio 15 Enterprise or higher:

```
git clone https://github.com/memmelma/MongOData_Csharp.git
```
## ProjectOneEF
Attempt to implement an OData interface on a MongoDB using the Entity Framework. Due to a lack of support, the solution is currently not working properly and therefore can be ignored!


## ProjectTwoNoEF
Attempt to implement an OData interface on a MongoDB using the C# and .NET MongoDB Driver provided by MongoDB Inc. under http://mongodb.github.io/mongo-csharp-driver/2.2/getting_started/installation/ .

In this solution two attempts are made with one lacking performance (currently reading the whole mongoDb collection instead of applying the query correctly) and with the other missing functionality (failing to apply the query and always materializing the whole collection with completely ignoring filters and such).


### Used packages
All packages are available via NuGet.


#### Microsoft.AspNet.OData
Version: v7.0.0-beta4
This package contains everything you need to create OData v4.0 endpoints using ASP.NET Web API and to support OData query syntax for your web APIs.

#### Microsoft.OData.Core
Version: v7.4.3
Classes to serialize, deserialize and validate OData JSON payloads. Supports OData v4 only. Enables construction of OData services and clients.

#### Microsoft.OData.Edm
Version: v5.6.0
Classes to represent, construct, parse, serialize and validate entity data models. Supports OData v4 only. 

#### MongoDB.Driver
Version: v2.6.1
Official .NET driver for MongoDB. - MongoDB C# and .NET Driver for connecting and using MongoDB istances

#### MongoDB.Bson
Version: v2.6.1
MongoDB's Official Bson Library. - Handling BSON format returned by MongoDB
