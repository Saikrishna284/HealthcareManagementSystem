import { isPlatformBrowser } from '@angular/common';
import { HttpInterceptorFn } from '@angular/common/http';
import { inject, PLATFORM_ID } from '@angular/core';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const platformId = inject(PLATFORM_ID);
  const isBrowser = isPlatformBrowser(platformId);
  
  const excludedUrls = ['/api/Auth/login'];
  
  if (excludedUrls.some(url => req.url.includes(url))) {
    return next(req);
  }
  
  if (isBrowser) {
    const token = localStorage.getItem('token');

    if (token) {
      const clonedReq = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
      return next(clonedReq);
    }
  }
  
  return next(req);

};
