import React, { Component } from 'react';
import briose from './briose.jpg'

export class RecipeDetails extends Component {
    static displayName = RecipeDetails.name;

    constructor(props) {
        super(props);
        this.state = { currentCount: 0 };
        this.incrementCounter = this.incrementCounter.bind(this);
    }

    incrementCounter() {
        this.setState({
            currentCount: this.state.currentCount + 1
        });
    }

    render() {
        return (
            <div>
                <h1>Briose (madlene)</h1>

                <p>Timp de preparare: 30 min</p>
                <p>Timp de coacere: 30 min sau pana cand o scobitoare introdusa in briose e uscata</p>

                <div>
                        <h3> Ingrediente</h3>
                        <ul>
                            <li>125g unt</li>
                            <li>250g zahar</li>
                            <li>4 oua</li>
                            <li>200g faina</li>
                            <li>rom</li>
                            <li>vanilie</li>
                            <li>putina apa minerala</li>
                            <li>1/2 lingurita praf de copt</li>
                        </ul>
                </div>

                <img src={briose} alt="poza cu briose" />

                <div>
                    <h3>Instructiuni</h3>
                    <ol>
                        <li>Intr-un vas se freaca untul cu zaharul si vanilia. Se adauga apa minerala, apoi galbenusurile pe rand, romul si faina, praful de copt.</li>
                        <li>Faina se puna cate putin si se amesteca foarte bine.__defineGetter__ Se bat albusurile spuma tare si se inglobeaza usor in crema de oua, amestecand cu lingura de lemn, de sus in jos.</li>
                        <li>Se pune aluatul in forme care se coc in cuptorul cald la foc potrivit.</li>
                    </ol>
                </div>
            </div>
        );
    }
}