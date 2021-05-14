import React, { Component } from 'react';

export class RecipeDetails extends Component {
    static displayName = RecipeDetails.name;

    render() {
        const ingredientList = this.props.recipe.ingredients.map((ingredient) =>
            <li>{ingredient}</li>
        );
        const instructionList = this.props.recipe.instructions.map((instruction) =>
            <li>{instruction}</li>  
        );
        const picturePath = this.props.recipe.picturePath;

        return (
            <div>
                <h1>{this.props.recipe.name} - {this.props.recipe.id}</h1>

                <p>Timp de preparare: {this.props.recipe.preparationTime}</p>
                <p>Timp de copt: {this.props.recipe.bakingTime}</p>

                <div>
                        <h3>Ingrediente</h3>
                        <ul> {ingredientList} </ul>
                </div>

                <img src={require("./briose.jpg")} alt="poza cu briose" />

                <div>
                    <h3>Instructiuni</h3>
                    <ol> {instructionList} </ol>
                </div>
            </div>
        );
    }
}