# PT-SalasDario

## Instructions to run the application:  
1) Extract the repository on your computer.  
2) Right-click on the project named **PT-SalasDario.API**.  
3) Select the option: **"Manage User Secrets"**.  
4) Paste the following content, replacing the values according to your configuration:  

    ```json
    {
      "ConnectionStrings:DefaultConnection": "Server=localhost;Port=3307;User Id=MY_USER;Database=MY_DB;Password=MY_PASSWORD;Connection Timeout=1000000"
    }
    ```

5) Make sure that the project named **PT-SalasDario.API** is set as the **Startup Project**.  
6) Press **F5** or click the **Run** button.  

## Important  

In the **console import project**, user secrets are not working. The connection string must be changed in **appsettings.json**.

