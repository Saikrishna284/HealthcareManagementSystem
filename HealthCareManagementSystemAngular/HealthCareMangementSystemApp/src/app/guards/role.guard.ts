import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const roleGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const requiredRole = route.data['requiredRole'];


  if (!authService.isLoggedIn()) {
    router.navigate(['/login']);
    return false;
  }


  const userRole = authService.getUserRoleFromToken();
  
  if (userRole === requiredRole || 
     (Array.isArray(requiredRole) && requiredRole.includes(userRole))) {
    return true;
  } else {
    router.navigate(['/login']);
    return false;
  }

};
