import React, { useState, useEffect } from 'react';
import { RecipeDetails } from './RecipeDetails';

export function RecipeDetailsFetcher(props) {

    const [recipe, setRecipe] = useState(null);

    useEffect(() => {
        fetch("http://localhost:6600/api/recipes/1")
            .then(x => x.json())
            .then(recipe => setRecipe(recipe));
       
        }, [props.id]);

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