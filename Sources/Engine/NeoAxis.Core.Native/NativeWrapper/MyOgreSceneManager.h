// Copyright (C) 2022 NeoAxis, Inc. Delaware, USA; NeoAxis Group Ltd. 8 Copthall, Roseau Valley, 00152 Commonwealth of Dominica.
#pragma once
using namespace Ogre;

class MyOgreSceneManager : public SceneManager
{
public:

	MyOgreSceneManager(const String& name) 
		: SceneManager(name) 
	{
	}
};
