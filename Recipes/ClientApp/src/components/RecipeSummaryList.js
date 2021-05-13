import React from 'react';
import { RecipeSummary } from './RecipeSummary';

export function RecipeSummaryList(props) {

    let recipeList = [];
    if (props.recipes.length !== 0 && props.recipes.every(x => x !== undefined)) {
        recipeList = props.recipes.map((recipe) =>
            <RecipeSummary recipe={recipe} key={recipe.id ?? 3} />
        );
    }
    

    console.log(recipeList);

    return (
        
        <ul>
            {recipeList}
        </ul>
    );
}