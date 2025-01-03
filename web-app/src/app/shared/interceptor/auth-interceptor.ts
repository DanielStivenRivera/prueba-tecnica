import { HttpInterceptorFn } from '@angular/common/http';


export const authInterceptor: HttpInterceptorFn = (req , next) => {
  const token = localStorage.getItem('atoken');
  if (token) {
    const request = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${token}`)
    });
    return next(request);
  }
  return next(req);
}
