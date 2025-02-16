# Beer2beer
# Theorie
- B2B Application fuer Restauraunts
- Entities
    - Customer
    - Article
    - User
    - Supplier
    - Order
- Relationships
    -  Order <----> OrderState
    -  Order <----> Customer
    -  Order ----->* Articles 
    
    - Article ----> Supplier
    
    - Supplier ----> Contact
    - Supplier ----> Address
    - Supplier ---->* Articles

- Services
   - 


## More
- Learn more about  Clean Architecture at  https://binarybytez.com/understanding- clean- architecture/ 
- Learn more about boilerplate api structure at  https://github.com/kawser2133/clean- structured- api- project/tree/development/
- Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
- Learn more about Fake data generation at  https://github.com/bchavez/Bogus
- Learn more about EF Migration at https://learn.microsoft.com/en- US/ef/core/managing- schemas/migrations/managing?tabs=dotnet- core- cli