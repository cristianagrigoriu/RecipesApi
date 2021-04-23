[![Build Status](https://dev.azure.com/CristianaGrigoriu/Recipes/_apis/build/status/cristianagrigoriu.RecipesApi?branchName=new-deploy)](https://dev.azure.com/CristianaGrigoriu/Recipes/_build/latest?definitionId=3&branchName=new-deploy)

# [RecipesApi](https://cg-recipes.azurewebsites.net/swagger/index.html)

A Recipes API to create and add your recipes

Initial design:

#### Actions

##### Recipes Controller
| Done | HTTP Verb | Path | Description | Response |
| ---- |----:| ----:| -----------:| --------: |
|<ul><li>- [x] </li></ul> | <span style="background-color: #61affe; border: none; font-family: sans-serif;font-size: 14px;  font-weight: 700;min-width: 80px;padding: 6px 15px;  text-align: center; border-radius: 3px; color: #fff;text-shadow: 0 1px 0 rgba(0,0,0,.1);"> GET </span>&nbsp;  | /recipes | get all recipes | 200 Ok |
|<ul><li>- [x] </li></ul> | <span style="background-color: #61affe; border: none; font-family: sans-serif;font-size: 14px;  font-weight: 700;min-width: 80px;padding: 6px 15px;  text-align: center; border-radius: 3px; color: #fff;text-shadow: 0 1px 0 rgba(0,0,0,.1);"> GET </span>&nbsp; | /recipes/{id} | get specific recipe - basic details (+ time) - by id -> short description and time | 200 Ok<br>404 Not Found |
|<ul><li>- [ ] </li></ul> | <span style="background-color: #61affe; border: none; font-family: sans-serif;font-size: 14px;  font-weight: 700;min-width: 80px;padding: 6px 15px;  text-align: center; border-radius: 3px; color: #fff;text-shadow: 0 1px 0 rgba(0,0,0,.1);"> GET </span>&nbsp; |/recipes/{id}/instructions<br>/recipes/{id}/ingredients | get specific recipe - all details - instruction & ingredients - by id | 200 Ok<br>404 Not Found |
|<ul><li>- [x] </li></ul> | <span style="background-color: rgb(73, 204, 144); border: none; font-family: sans-serif;font-size: 14px;  font-weight: 700;min-width: 80px;padding: 6px 15px;  text-align: center; border-radius: 3px; color: #fff;text-shadow: 0 1px 0 rgba(0,0,0,.1);"> POST</span>&nbsp;  | /recipes<br>{name:, description:, category: breakfast/lunch/dinner} | add recipe - all details | 201 Created + new object<br>400 Bad request - validation error |
|<ul><li>- [x] </li></ul> | <span style="background-color: rgb(252, 161, 48); border: none; font-family: sans-serif;font-size: 14px;  font-weight: 700;min-width: 80px;padding: 6px 15px;  text-align: center; border-radius: 3px; color: #fff;text-shadow: 0 1px 0 rgba(0,0,0,.1);"> PUT</span>&nbsp; | /recipes/{id}<br>{newName:...} | update recipe - basic details | 204 No Content<br>400 Bad request - validation error<br>404 Not Found |
|<ul><li>- [ ] </li></ul> | <span style="background-color: rgb(213, 128, 58); border: none; font-family: sans-serif;font-size: 14px;  font-weight: 700;min-width: 80px;padding: 6px 15px;  text-align: center; border-radius: 3px; color: #fff;text-shadow: 0 1px 0 rgba(0,0,0,.1);"> PATCH</span>&nbsp; | /recipes/{id}/instructions<br>{instructions:...} | update instructions | 204 No Content<br>400 Bad request - validation error<br>404 Not Found |
|<ul><li>- [ ] </li></ul> | <span style="background-color: #f93e3e; border: none; font-family: sans-serif;font-size: 14px;  font-weight: 700;min-width: 80px;padding: 6px 15px;  text-align: center; border-radius: 3px; color: #fff;text-shadow: 0 1px 0 rgba(0,0,0,.1);"> DELETE </span> &nbsp; | /recipes/{id} | delete recipe -> and everything related | 204 No Content<br>404 Not Found |
|<ul><li>- [ ] </li></ul> | <span style="background-color: #61affe; border: none; font-family: sans-serif;font-size: 14px;  font-weight: 700;min-width: 80px;padding: 6px 15px;  text-align: center; border-radius: 3px; color: #fff;text-shadow: 0 1px 0 rgba(0,0,0,.1);"> GET </span>&nbsp; |/recipes?categories=...&ingredients=...&keyword=... | filter recipes<br>filter only after what is filled in, otherwise return everything | 200 Ok |

<span style="background-color: #f93e3e; border: none; font-family: sans-serif;font-size: 14px;  font-weight: 700;min-width: 80px;padding: 6px 15px;  text-align: center; border-radius: 3px; color: #fff;text-shadow: 0 1px 0 rgba(0,0,0,.1);"> DELETE </span>

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

