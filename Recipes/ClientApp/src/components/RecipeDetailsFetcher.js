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
            id: this.props.id,
            title: "Brioșe delicioase (madlene)",
            preparationTime: "Timp de preparare: 30 min",
            bakingTime: "Timp de coacere: 30 min sau pana cand o scobitoare introdusa in briose e uscata",
            picturePath: "./briose.jpg",
            ingredients: [
                "125g unt",
                "250g zahar",
                "4 oua",
                "200g faina",
                "rom",
                "vanilie",
                "putina apa minerala",
                "1/2 lingurita praf de copt"
            ],
            instructions: [
                "Intr-un vas se freaca untul cu zaharul si vanilia. Se adauga apa minerala, apoi galbenusurile pe rand, romul si faina, praful de copt.",
                "Faina se puna cate putin si se amesteca foarte bine. Se bat albusurile spuma tare si se inglobeaza usor in crema de oua, amestecand cu lingura de lemn, de sus in jos.",
                "Se pune aluatul in forme care se coc in cuptorul cald la foc potrivit."
            ]
        };

        setTimeout(() => {
                this.setState({ loadingStatus: "Loaded", recipe })
            },
            1000);
    }

    render() {
        if (this.state.loadingStatus === "Loaded") {
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