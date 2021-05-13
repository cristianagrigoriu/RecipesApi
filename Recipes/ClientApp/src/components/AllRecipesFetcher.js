import React, { useState, useEffect } from 'react';
import { RecipeSummaryList } from './RecipeSummaryList';

export function AllRecipesFetcher(props) {
    const [recipes, setRecipes] = useState([]);

    useEffect(() => {
        fetch(`http://localhost:6600/api/recipes`)
            .then(x => x.json())
            //.then(y => {
            //    console.log('AllRecipesFetcher');
            //    console.log(y);
            //})
            .then(recipes => recipes.map(recipe => setRecipes([...recipes, recipe.props])));
    }, []);

    if (recipes !== null) {
        console.log(recipes)
        return (
            <div>
                <RecipeSummaryList recipes={recipes.filter(x => x !== undefined)} />
            </div>
        );
    }
    else {
        return <p>Your recipes will arrive shortly...</p>;
    }
}