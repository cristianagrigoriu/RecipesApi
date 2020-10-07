# RecipesApi

A Recipes API to create and add your recipes

Initial design:

#### Actions

##### Recipes Controller
| Done | Path | Description | Response |
| ---- |----:| -----------:| --------: |
|<ul><li>- [x] </li></ul> | /recipes | get all recipes | 200 Ok |
|<ul><li>- [x] </li></ul> | /recipes/{id} | get specific recipe - basic details (+ time) - by id -> short description and time | 200 Ok<br>404 Not Found |
|<ul><li>- [ ] </li></ul> | /recipes/{id}/instructions<br>/recipes/{id}/ingredients | get specific recipe - all details - instruction & ingredients - by id | 200 Ok<br>404 Not Found |
|<ul><li>- [x] </li></ul> | /recipes<br>{name:, description:, category: breakfast/lunch/dinner} | add recipe - all details | 201 Created + new object<br>400 Bad request - validation error |
|<ul><li>- [x] </li></ul> | /recipes/{id}<br>{newName:...} | update recipe - basic details | 204 No Content<br>400 Bad request - validation error<br>404 Not Found |
|<ul><li>- [ ] </li></ul> | /recipes/{id}/instructions<br>{instructions:...} | update instructions | 204 No Content<br>400 Bad request - validation error<br>404 Not Found |
|<ul><li>- [ ] </li></ul> | /recipes/{id} | delete recipe -> and everything related | 204 No Content<br>404 Not Found |
|<ul><li>- [ ] </li></ul> | /recipes?categories=...&ingredients=...&keyword=... | filter recipes<br>filter only after what is filled in, otherwise return everything | 200 Ok |

##### Ingredients Controller
| Done | Path | Description | Response |
| ---- |----:| -----------:| --------: |
|<ul><li>- [ ] </li></ul> | /ingredients | get all ingredients | 200 Ok |
|<ul><li>- [ ] </li></ul> | /ingredients<br>{name:} | add ingredient | 201 Created<br>400 Bad Request |
|<ul><li>- [ ] </li></ul> | /ingredients/{id} | delete ingredient | 204 No Content<br>404 Not Found |


#### To include
- [ ] Validations

- [x] asp.net core 3

- [x] use postman collection

- [x] configure swagger - use as documentation

- [ ] (unit tests)

- [ ] repository in memory/file
- [ ] (Entity Framework - SQLite - use local file)

