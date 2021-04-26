import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { RecipeDetailsFetcher } from './components/RecipeDetailsFetcher';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {

        return (
          <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/counter' component={Counter} />
            <Route path='/fetch-data' component={FetchData} />
                <Route path='/recipe-details'
                    render={(props) => (
                        <RecipeDetailsFetcher {...props} id={1} />
                  )} />
          </Layout>
        );
  }
}
