import { Action } from 'redux';

export interface DeleteMessageAction extends Action {
  index: number;
}
