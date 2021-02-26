export const ApplicationName = 'AgoraSocialApp';

export const AUTH_CONTEXT = {};

export const ReturnUrlType = 'returnUrl';

export const QueryParameterNames = {
  ReturnUrl: ReturnUrlType,
  Message: 'message'
};

export const LogoutActions = {
  LogoutCallback: '/authentication/logout-callback',
  Logout: '/authentication/logout',
  LoggedOut: '/authentication/logged-out'
};

export const LoginActions = {
  Login: '/authentication/login',
  LoginCallback: '/authentication/login-callback',
  LoginFailed: '/authentication/login-failed',
  Profile: '/authentication/profile',
  Register: '/authentication/register'
};

let applicationPaths: ApplicationPathsType = {
  DefaultLoginRedirectPath: '/',
  ApiAuthorizationClientConfigurationUrl: `/_configuration/${ApplicationName}`,
  Login: `${LoginActions.Login}`,
  LoginFailed: `${LoginActions.LoginFailed}`,
  LoginCallback: `${LoginActions.LoginCallback}`,
  Register: `${LoginActions.Register}`,
  Profile: `${LoginActions.Profile}`,
  LogOut: `${LogoutActions.Logout}`,
  LoggedOut: `${LogoutActions.LoggedOut}`,
  LogOutCallback: `${LogoutActions.LogoutCallback}`,
  LoginPathComponents: [],
  LoginFailedPathComponents: [],
  LoginCallbackPathComponents: [],
  RegisterPathComponents: [],
  ProfilePathComponents: [],
  LogOutPathComponents: [],
  LoggedOutPathComponents: [],
  LogOutCallbackPathComponents: [],
  IdentityRegisterPath: 'Identity/Account/Register',
  IdentityManagePath: 'Identity/Account/Manage'
};

applicationPaths = {
  ...applicationPaths,
  LoginPathComponents: applicationPaths.Login.split('/'),
  LoginFailedPathComponents: applicationPaths.LoginFailed.split('/'),
  RegisterPathComponents: applicationPaths.Register.split('/'),
  ProfilePathComponents: applicationPaths.Profile.split('/'),
  LogOutPathComponents: applicationPaths.LogOut.split('/'),
  LoggedOutPathComponents: applicationPaths.LoggedOut.split('/'),
  LogOutCallbackPathComponents: applicationPaths.LogOutCallback.split('/')
};

interface ApplicationPathsType {
  readonly DefaultLoginRedirectPath: string;
  readonly ApiAuthorizationClientConfigurationUrl: string;
  readonly Login: string;
  readonly LoginFailed: string;
  readonly LoginCallback: string;
  readonly Register: string;
  readonly Profile: string;
  readonly LogOut: string;
  readonly LoggedOut: string;
  readonly LogOutCallback: string;
  readonly LoginPathComponents: string [];
  readonly LoginFailedPathComponents: string [];
  readonly LoginCallbackPathComponents: string [];
  readonly RegisterPathComponents: string [];
  readonly ProfilePathComponents: string [];
  readonly LogOutPathComponents: string [];
  readonly LoggedOutPathComponents: string [];
  readonly LogOutCallbackPathComponents: string [];
  readonly IdentityRegisterPath: string;
  readonly IdentityManagePath: string;
}

export const ApplicationPaths: ApplicationPathsType = applicationPaths;
