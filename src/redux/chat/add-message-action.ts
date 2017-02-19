import { Action } from 'redux';

export interface AddMessageAction extends Action {
  message: string;
}
