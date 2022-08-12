pipeline {
	agent any
	
	environment {
		registryCredential = "classifieds-user"

    }
   
    stages {

    // Tests
    stage('Unit Tests') {
      steps{
        script {
          echo 'testing stage...'

        }
      }
    }
        
    // Building Docker images
    stage('Building image') {
      steps{
        script {
        //stop old containers
            //sh 'docker-compose –f docker-compose.yml down -v'
            //sh 'docker-compose down'
            sh 'docker-compose build'
            echo 'Docker-compose-build Build Image Completed'   

        }
      }
    }
   
    // Uploading Docker images into AWS ECR
    stage('Pushing to ECR') {
     steps{  
         script {
			docker.withRegistry("https://" + ${env.REPOSITORY_URI}, "ecr:${env.AWS_DEFAULT_REGION}:" + registryCredential) {
                        sh 'docker push ${env.REPOSITORY_URI}:be'
                        sh 'docker push ${env.REPOSITORY_URI}:fe'


                        echo 'finished pushing to ECR...'
                	}
         }
        }
      }
      
    stage('Deploy') {
     steps{
            withAWS(credentials: registryCredential, region: "${env.AWS_DEFAULT_REGION}") {
                script {
			      //  sh 'docker-compose up'

                    echo 'finished deploying containers...'
                }
            }
        }      
      }
    }
}