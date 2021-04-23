import React, { Component } from 'react';
import { RecipeDetails } from './RecipeDetails';

export class RecipeDetailsFetcher extends Component {

    constructor(props) {
        super(props);
        this.state = {
            loadingStatus: "NotLoaded",
            recipe: null
        };
    }

    componentDidMount() {
        const recipe = {
            title: "Brioșe delicioase (madlene)"
        };

        setTimeout(() => {
                this.setState({ loadingStatus: "Loaded", recipe })
            },
            1000);
    }

    render() {
        if (this.state.loadingStatus == "Loaded") {
            return (
                <RecipeDetails recipe={this.state.recipe} />
            );
        }
        else
        {
            return <p>Your recipe will arrive shortly...</p>;
        }
    }
}