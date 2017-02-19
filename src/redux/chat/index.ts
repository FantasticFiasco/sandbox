import {
  Action,
  Reducer,
  Store,
  createStore
} from 'redux';

import { AddMessageAction } from './add-message-action';
import { AppState } from './app-state';
import { DeleteMessageAction } from './delete-message-action';
import { MessageActions } from './message-actions';

const initialState: AppState = { messages: [] };

const reducer: Reducer<AppState> =
  (state: AppState = initialState, action: Action) => {
    switch (action.type) {
      case 'ADD_MESSAGE':
        return {
          messages: state.messages.concat((<AddMessageAction>action).message),
        }
      case 'DELETE_MESSAGE':
        const index = (<DeleteMessageAction>action).index;
        return {
          messages: [
            ...state.messages.slice(0, index),
            ...state.messages.slice(index + 1, state.messages.length)
          ]
        }
      default:
        return state;
    }
  };

const store: Store<AppState> = createStore<AppState>(reducer);
console.log(store.getState()); // -> { messages: [] }

store.dispatch(MessageActions.addMessage('Would you say the fringe was made of silk?'));

store.dispatch(MessageActions.addMessage('Wouldnt have no other kind but silk'));

store.dispatch(MessageActions.addMessage('Has it really got a team of snow white horses?'));

console.log(store.getState());
// ->
// { messages:
//    [ 'Would you say the fringe was made of silk?',
//      'Wouldnt have no other kind but silk',
//      'Has it really got a team of snow white horses?' ] }
