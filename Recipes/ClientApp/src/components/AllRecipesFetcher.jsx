import React, { useState, useEffect } from 'react';
import { RecipeSummaryList } from './RecipeSummaryList';

export function AllRecipesFetcher(props) {
    const [recipes, setRecipes] = useState();


    useEffect(() => {
        fetch('/api/recipes')
            .then(x => x.json())
            //.then(y => {
            //    console.log('AllRecipesFetcher');
            //    console.log(y);
            //})
            .then(recipes => setRecipes(recipes));
    }, []);

    if (recipes === undefined) {
        return (<p> Waiting for recipes </p>);
    } else if (recipes.length === 0) {
        return (<p> Oh no! There are no recipes ! </p>);
    } else {
        return (
            <div>
                <RecipeSummaryList recipes={recipes}/>
            </div>
        );
    }
}