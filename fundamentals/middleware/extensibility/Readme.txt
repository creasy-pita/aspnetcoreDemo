Project MyPipeline 
	���� ������ܵ���requestdelegate)

Project MiddlewareExtensibilitySample1
	1 �Զ���middleware
	2 �Զ���middleware,����usemiddleware<T> �����Ǵ������
	��������չ������ʹ��
		��̬��������̬���������������д��� this class

Project MiddlewareExtensibilitySample2
	Pre-request dependencies: 
		IMyScopedService is injected into invoke

		IMyScopedService is injected into Constructor
			����Cannot resolve scoped service 'MiddlewareExtensibilitySample2.IMyScopedService' from root provider.
			Scoped service �� pre-request �д����� ��Constructor ��webhostbuilder ��ʼ��������
			ת�� AddSingleton ����ע�����Ͳ��ᱨ��
	