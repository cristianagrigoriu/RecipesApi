import React from 'react';
import { RecipeSummary } from './RecipeSummary';

export function RecipeSummaryList(props) {

    let recipeList = props.recipes.map((recipe) =>
        <RecipeSummary recipe={recipe} key={recipe.id} />
    );
    

    console.log(recipeList);

    return (
        <ul>
            {recipeList}
        </ul>
    );
}