pipeline {
	agent any
	
	environment {
        SECRET_FILE_ID = credentials('env-secrets')
		registryCredential = "classifieds-user"

    }
   
    stages {

    // Tests
    stage('Unit Tests') {
      steps{
        script {
          //sh 'npm install'
	      //sh 'npm test -- --watchAll=false'
          echo 'environment variables...'
          echo ${env.AWS_ACCOUNT_ID}
          echo ${env.AWS_DEFAULT_REGION}
          echo ${env.CLUSTER_NAME}
          echo ${env.SERVICE_NAME}
          echo ${env.TASK_DEFINITION_NAME}
          echo ${env.DESIRED_COUNT}
          echo ${env.IMAGE_REPO_NAME}
          echo ${env.REPOSITORY_URI}

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
            //sh 'docker-compose build'
            echo 'Docker-compose-build Build Image Completed'   

        }
      }
    }
   
    // Uploading Docker images into AWS ECR
    stage('Pushing to ECR') {
     steps{  
         script {
			docker.withRegistry("https://" + REPOSITORY_URI, "ecr:${AWS_DEFAULT_REGION}:" + registryCredential) {
                       // sh 'docker push ${REPOSITORY_URI}:be'
                       // sh 'docker push ${REPOSITORY_URI}:fe'


                        echo 'finished pushing to ECR...'
                	}
         }
        }
      }
      
    stage('Deploy') {
     steps{
            withAWS(credentials: registryCredential, region: "${AWS_DEFAULT_REGION}") {
                script {
			      //  sh 'docker-compose up'

                    echo 'finished deploying containers...'
                }
            }
        }      
      }
    }
}