# PlatformWellAPIReader
This project will read PlatformWellActual API data and save to database and load data into table for view

 1. Clone or download the project
 2. Rebuild the solution
 3. Open windows power shell as administrator mode
 4. Go to projects DataAccessLayer
 5. Run this following command
    a. dotnet ef database update 0
    b. dotnet ef migrations remove
    c. dotnet ef migrations add platformwell
    d. dotnet ef database update
 6. Go to Visual Studio and click right button and set PlatformWell as "Set as Startup Project"
 7. Run the projects
 8. Give the API url: http://test-demo.aem-enersol.com/api/PlatformWell/GetPlatformWellActual
 9. Click the button "Save API Data and Load"
 10. Button click event will read the API data and save to database and Load the data into table.
 11. If you give wrong url like http://test-demo.aem-enersol.com/api/PlatformWell/GetPlatformWellDummy which entities are not matching then an alert will show
 12. SQL query named "LastUpdatedWellForEachPlatformQuery.sql" which will return last updated well for each platform.
