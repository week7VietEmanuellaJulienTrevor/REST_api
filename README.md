## Rocket Elevators REST API

This week we were tasked with creating a Rest Api for rocket elevators.
Using C# and .NET Core.

The different models connect to the preexisting MySQL database established in the rails app from previous weeks.

The app is deployed on Azure services and can be accessed through this URL: 
https://rocketelevatorsstatus-restapi.azurewebsites.net/

The different models and Controllers allow us to access and modify in some cases specific information.

## 

 **The different end points can be tested on an application such as postman (Click on the button below):**

### Batteries **(GET)**
* To retrieve the list of all Batteries:
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/batteries
* To retrieve all information of a specific Battery
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/batteries/8
* To retrieve the current status of a specific Battery
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/batteries/status/8

### Columns **(GET)**
* To retrieve the list of all Columns:
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/columns  
* To retrieve all information of a specific Column
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/columns/8
* To retrieve the current status of a specific Column
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/columns/status/8

### Elevators **(GET)**
* To retrieve the list of all Elevators:
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/elevators
* To retrieve all information of a specific Elevator
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/elevators/8
* To retrieve the current status of a specific Elevator
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/elevators/status/8

the status of these elements can be changed by using a **PUT** method to: 
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/elevators/8/status
*changing the Id number to the desired elevator*
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/columns/8/status
*changing the Id number to the desired column*
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/batteries/8/status
*changing the Id number to the desired battery*

3 more **GET** methods can be sent to these URLs to get specific ninformation like elevators that are not functioning, https://rocketelevatorsstatus-restapi.azurewebsites.net/api/elevators/not-operating

Buildings that require an intervention : 
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/buildings/needing-intervention

and Recent leads that have yet to be converted to customers:
https://rocketelevatorsstatus-restapi.azurewebsites.net/api/leads/open-leads

they can all be accessed on a postman collection that when run will execute a sequence retrieving and changing the information before restoring for further tests. (supplied in the codeboxx deliverable)



[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/b5d53c3b6dfeabcc3e0c)




