import React from 'react';

export function RecipeSummary(props) {
    return (
        <li>
            <div>{props.recipe.id}</div>
            <div><a href={`/recipe-details/${props.recipe.id}`}> {props.recipe.name}</a></div>
        </li>
    );
}