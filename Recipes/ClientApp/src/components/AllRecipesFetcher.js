import React, { useState, useEffect } from 'react';
import { RecipeSummary } from './RecipeSummary';

export function AllRecipesFetcher(props) {
    const [recipes, setRecipes] = useState([]);

    useEffect(() => {
        fetch(`http://localhost:6600/api/recipes`)
            .then(x => x.json())
            .then(recipe => setRecipes(recipes));

    }, []);

    if (recipes !== null) {
        return (
            <div>
                <RecipeSummary recipes={recipes} />
            </div>
        );
    }
    else {
        return <p>Your recipes will arrive shortly...</p>;
    }
}