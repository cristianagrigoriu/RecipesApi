import React from 'react';

export function RecipeSummary(props) {
    return (
        <li>
            <div>{props.recipe.id}</div>
            <div>{props.recipe.name}</div>
        </li>
    );
}