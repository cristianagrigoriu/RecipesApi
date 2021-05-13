import React, { useState, useEffect } from 'react';
import { RecipeDetails } from './RecipeDetails';

export function RecipeDetailsFetcher(props) {

    const [recipe, setRecipe] = useState(null);

    useEffect(() => {
        fetch(`http://localhost:6600/api/recipes/${props.match.params.recipeId}`)
            .then(x => x.json())
            .then(recipe => setRecipe(recipe));
       
        }, [props.recipeId]);

    if (recipe !== null) {
        return (
            <RecipeDetails recipe={recipe} />
        );
    }
    else
    {
        return <p>Your recipe will arrive shortly...</p>;
    }
}