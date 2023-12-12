Microsoft DDD/TDD mimarisi dikkate alınarak oluşturulmuştur.

#Documents - Guide/Techincal notes&tips
#Common - Common Layer
#Core - Core Layer
#BLL - Busniess Logic Layer
#DLL - Data Access Layer
#Presentation - Presentation Layer

Dependency Priorty Order = (Core->DAL->BLL) 
			   (Common = Cross Layer)

//Solution içindeki bağımlılıklar self-container life cycle optimizasyonu yapmadan "IntegratedRegisterService"
içinde dahil edilmiştir.

1) Swagger arayüzüne aşağıdaki url'den erişilebilir.

http://localhost:5114/swagger/index.html

2) Dönen Resultlar "ApiResult" Generic sınıfından meydana gelir.
Bu sınıfta "IsError" prop. true ise, bir sorun vardır ve "msg" prop.'da sorun yazar.

3) "Session Expire" süresi "5 min." dir.
(BLL->Services->LoginService katmanından property degistirilebilir.)

4) Kampanya servisleri
(BLL->Services->CampaignServices)

5) End pointleri test etmek için; 
(Presentation->TheaTechEComAPI->TestEndPoints) uzantıları kullanılabilir.

