import { Action } from './action';
import { Reducer } from './Reducer';
import { Store } from './store';

const reducer: Reducer<number> = (state: number, action: Action) => {
  switch (action.type) {
    case 'INCREMENT':
        return state + 1;
    case 'DECREMENT':
        return state - 1;
    case 'PLUS':
        return state + action.payload;
    default:
        return state; // <-- dont forget!
  }
};

/**
 * Actions
 */

const incrementAction: Action = { type: 'INCREMENT' }
console.log(reducer(0, incrementAction)); // -> 1
console.log(reducer(1, incrementAction)); // -> 2

const decrementAction: Action = { type: 'DECREMENT' }
console.log(reducer(100, decrementAction)); // -> 99

const unknownAction: Action = { type: 'UNKNOWN' };
console.log(reducer(100, unknownAction)); // -> 100

console.log(reducer(3, { type: 'PLUS', payload: 7 }));    // -> 10 
console.log(reducer(3, { type: 'PLUS', payload: 9000 })); // -> 9003 
console.log(reducer(3, { type: 'PLUS', payload: -2 }));   // -> 1

/**
 * Store
 */

const store = new Store<number>(reducer, 0);
console.log(store.getState()); // -> 0
 
// subscribe
const unsubscribe = store.subscribe(() => {
  console.log('subscribed: ', store.getState());
});
 
store.dispatch({ type: 'INCREMENT' }); // -> subscribed: 1
store.dispatch({ type: 'INCREMENT' }); // -> subscribed: 2
 
unsubscribe();
store.dispatch({ type: 'DECREMENT' }); // (nothing logged)
 
// decrement happened, even though we weren't listening for it
console.log(store.getState()); // -> 1