formatPhoneNumber = (phoneNumber) => 
      phoneNumber
      .replace(/\D/g, '')
      .replace(/(\d)/, '($1')
      .replace(/(\d{2})(\d)/, '$1) $2')
      .replace(/(\d{4})(\d)/, '$1-$2')
      .replace(/(\d)(\d{3})-(\d)(\d{4})/, '$1 $2$3-$4')
      .replace(/(-\d{4})\d+?$/, '$1');