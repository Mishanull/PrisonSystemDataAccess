# Prison System Data Access
This repository is part of a larger, 3-tier system. All the details about the entire system can be found on the presentation tier repository: https://github.com/Mishanull/PrisonSystemPresentation

# Design of this Tier (Class Diagram)
![DataAccessDiagram](https://github.com/Mishanull/PrisonSystemDataAccess/blob/03ad007ba9a7fcead19297a64731c96d0e60e2c9/Appendix-F_Data%20Access%20Tier%20Class%20Diagram.svg)
  In this tier’s diagram, since it is in the .NET environment, subsystems are used, as in 
the presentation tier. There are 5 subsystems in this diagram, namely: DataAccessAPI, 
Interfaces, EfcData, FileContext, and Entities.
  The DataAccessAPI subsystem represents the Web API in itself, with its REST 
Controller classes, that are used to handle calls to the endpoints from the application 
tier, hence they are also named after each entity type that they represent and have 
operations that reflect it.
  The Interfaces subsystem contains all the necessary interfaces, prefixed with 
Service, that are used by the controllers to access the data persistence layer. These 
interfaces are further implemented by the classes in both FileContext and EfcData.
Efcdata contains the Data Access Object classes for each entity, which use the 
PrisonSystemContext class as means of accessing the database. This class is used by 
Entity Framework Core to create database tables out of entities and handle operations 
on them. It extends the DbContext interface, provided by EFC.
FileData is only used to log to a file, whenever an alert is broadcast. It is similar to 
EfcData, except it uses its own defined Context class to store logs.
